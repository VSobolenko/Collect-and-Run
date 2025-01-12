using System.Collections.Generic;
using Game.Shops;
using GameAnalyticsSDK;

namespace Runner.Analytics
{
public class GameAnalyticsDirector : IAnalyticsDirector
{
    public GameAnalyticsDirector()
    {
        InitializeGameAnalytics();
    }

    private void InitializeGameAnalytics()
    {
        GameAnalytics.Initialize();
    }

    public void LevelStart(int levelId)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, $"Level_Start_{levelId}", levelId,
                                          new Dictionary<string, object>
                                          {
                                              {"LevelId", levelId},
                                          });
    }

    public void LevelVictory(int levelId)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, $"Level_Complete_{levelId}", levelId,
                                          new Dictionary<string, object>
                                          {
                                              {"LevelId", levelId},
                                          });
    }

    public void LevelDefeat(int levelId)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, $"Level_Fail_{levelId}", levelId,
                                          new Dictionary<string, object>
                                          {
                                              {"LevelId", levelId},
                                          });
    }

    public void PurchaseResponse(PurchaseResponseResult result, GameProduct product)
    {
        GameAnalytics.NewBusinessEvent("USD", 1, "Support",
                                       product.ProductId, "SomeCardType", new Dictionary<string, object>
                                       {
                                           {"Result", result.result},
                                           {"Message", result.message},
                                       });
    }
}
}