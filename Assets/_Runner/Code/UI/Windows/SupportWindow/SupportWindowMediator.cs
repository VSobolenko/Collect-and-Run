using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Game;
using Game.GUI.Windows;
using Game.Shops;
using Runner.Analytics;

// ReSharper disable CoVariantArrayConversion

namespace Runner.UI.SupportWindow
{
public class SupportWindowMediator : BaseReactiveMediator<SupportWindowView, SupportWindowAction>
{
    private readonly WindowsDirector _windowsDirector;
    private readonly ShopDirector _shopDirector;
    private readonly IAnalyticsDirector _analyticsDirector;
    private readonly CancellationTokenSource _cancellationTokenSource = new();

    public SupportWindowMediator(SupportWindowView window, WindowsDirector windowsDirector, ShopDirector shopDirector,
                                 IAnalyticsDirector analyticsDirector) :
        base(
            window, window.windowButtons)
    {
        _windowsDirector = windowsDirector;
        _shopDirector = shopDirector;
        _analyticsDirector = analyticsDirector;
    }

    public override void OnInitialize()
    {
        SubscribeToWindowsButtons();
    }

    public override void OnDestroy()
    {
        UnsubscribeToWindowsButtons();
        _cancellationTokenSource.Cancel();
        _cancellationTokenSource.Dispose();
    }

    protected override void ProceedButtonAction(SupportWindowAction action)
    {
        switch (action)
        {
            case SupportWindowAction.Unknown:
                throw new ArgumentOutOfRangeException(nameof(action), action, null);
            case SupportWindowAction.OnClickClose:
                ProcessCloseThisWindow();

                break;
            case SupportWindowAction.OnClickSimple:
                ProcessSimpleBought();

                break;
            case SupportWindowAction.OnClickIntermediate:
                ProcessIntermediateBought();

                break;
            case SupportWindowAction.OnClickAdvanced:
                ProcessAdvancedBought();

                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(action), action, null);
        }
    }

    private void ProcessCloseThisWindow() => _windowsDirector.CloseWindow(this);

    private async void ProcessSimpleBought() => await PurchaseProduct(0);

    private async void ProcessIntermediateBought() => await PurchaseProduct(1);

    private async void ProcessAdvancedBought() => await PurchaseProduct(2);

    private async Task PurchaseProduct(int productIndex)
    {
        if (_shopDirector.ShopManager.Products.Count == 0)
        {
            Log.Error("Null Products");

            return;
        }

        SetInteraction(false);
        var product = _shopDirector.ShopManager.Products.ToList()[productIndex];
        var result =
            await _shopDirector.ShopManager.PurchaseProduct(product.ProductId);
        _analyticsDirector.PurchaseResponse(result, product);

        if (_cancellationTokenSource.IsCancellationRequested)
            return;
        ProcessResult(result);
        SetInteraction(true);
    }

    private void ProcessResult(PurchaseResponseResult result)
    {
        switch (result.result)
        {
            case PurchaseResult.Success:
                window.ShowInformer("Thank you");

                return;
        }

        Log.Error($"Purchase Error: Result:{result.result}; Message:{result.message}");
        window.ShowInformer("An error occurred, please try again later");
    }
}
}