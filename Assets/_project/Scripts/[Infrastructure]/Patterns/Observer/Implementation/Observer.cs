using System;
using UnityEngine;
namespace _Project.Scripts._Infrastructure_.Patterns.Observers
{
    public class Observer : MonoBehaviour, IObserverCallbackable, IObserverListenable
    {
        private event Action Event;

        public void Callback() 
            => Event?.Invoke();

        public void Subscribe(Action action) => 
            Event += action;

        public void Unsubscribe(Action value) => 
            Event -= value;
    }
}
