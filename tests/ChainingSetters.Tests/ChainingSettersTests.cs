using System;
using Xunit;

namespace ChainingSetters.Tests
{
    public class ChainingSettersTests
    {
        [Fact]
        public void ShouldInitFieldsByChaining()
        {
            var dog = new Hero()
                .SetName("Dog")
                .SetEventName("Race of mercy")
                .SetWhenEventHappend(new DateTime(1925, 1, 1));

            Assert.Equal("Hero(Dog, Race of mercy, 1925)", dog.ToString());
        }

        [Fact]
        public void ShouldInitFieldsStepByStep()
        {
            var dog = new Hero();
            dog.Name = "Dog";
            dog.EventName = "Race of mercy";
            dog.EventHappend = new DateTime(1925, 1, 1);

            Assert.Equal("Hero(Dog, Race of mercy, 1925)", dog.ToString());
        }
    }
}
