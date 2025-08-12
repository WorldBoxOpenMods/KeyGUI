using System.Reflection;
using JetBrains.Annotations;

namespace KeyGUI.MenuArchitecture {
  public abstract class KeyGuiPatch {
    public abstract MethodInfo TargetMethod { get; }
    [CanBeNull] public virtual MethodInfo Prefix => null;
    [CanBeNull] public virtual MethodInfo Postfix => null;
    [CanBeNull] public virtual MethodInfo Transpiler => null;
    [CanBeNull] public virtual MethodInfo Finalizer => null;
    [CanBeNull] public virtual MethodInfo IlManipulator => null;
  }
}
