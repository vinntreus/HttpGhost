using System;

namespace HttpGhost.Navigation
{
    [Serializable]    
    public class NavigationResultException : Exception
    {
        public NavigationResultException(string message): base(message){}
    }
}