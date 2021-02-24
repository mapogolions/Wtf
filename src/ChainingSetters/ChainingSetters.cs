using System;

namespace ChainingSetters
{
    public class Hero
    {
        public string Name { get; set; }
        public string EventName { get; set; }
        public DateTime EventHappend { get; set; }

        public override string ToString() => $"{nameof(Hero)}({Name}, {EventName}, {EventHappend.Date.Year})";
    }

    public static class HeroExtensions
    {
        public static Hero SetName(this Hero hero, string name)
        {
            hero.Name = name;
            return hero;
        }

        public static Hero SetEventName(this Hero hero, string eventName)
        {
            hero.EventName = eventName;
            return hero;
        }

        public static Hero SetWhenEventHappend(this Hero hero, DateTime eventHappend)
        {
            hero.EventHappend = eventHappend;
            return hero;
        }
    }
}
