using System;
namespace _Project.Scripts._Infrastructure_.Patterns.Observers
{
    public interface IObserverListenable
    {
        void Subscribe(Action action);
        void Unsubscribe(Action action);
    }
}