using System;

namespace HttpGhost.Navigation
{
    public class NavigationResultException : Exception
    {
        public NavigationResultException(string message): base(message){}
    }
}