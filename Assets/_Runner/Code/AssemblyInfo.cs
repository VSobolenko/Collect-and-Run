using System;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: AssemblyTitle("Runner")] //Visible internal class/struct for test
[assembly: AssemblyVersion("1.0.0")]
[assembly: InternalsVisibleTo("Runner.Tests")] //Visible internal class/struct for test
[assembly: InternalsVisibleTo("Runner.Editor")]
[assembly: CLSCompliant(false)]