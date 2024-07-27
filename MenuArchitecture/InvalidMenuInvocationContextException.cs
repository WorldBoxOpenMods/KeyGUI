using System;

namespace KeyGUI.MenuArchitecture {
  public class InvalidMenuInvocationContextException : ApplicationException {
    internal InvalidMenuInvocationContextException() {
    }
    internal InvalidMenuInvocationContextException(string message) : base(message) {
    }
    internal InvalidMenuInvocationContextException(string message, Exception innerException) : base(message, innerException) {
    }
  }
}