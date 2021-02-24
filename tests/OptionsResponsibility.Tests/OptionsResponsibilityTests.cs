using OptionsResponsiblity;
using System;
using Xunit;

namespace OptionsResponsibility.Tests
{
    public class OptionsResponsibilityTests
    {
        // incosistent behaviour depending on selected approach
        [Fact]
        public void ShouldThrowEntryLevelException()
        {
            static void action()
            {
                var entry = new WeightlessExpirableEntry
                {
                    AbsoluteExpiration = new DateTime(1925, 1, 1),
                    RelativeToNowExpiration = TimeSpan.FromSeconds(10),
                };
                entry.Size = -1;
            }

            Assert.Throws<InvalidOperationException>(action);
        }

        [Fact]
        public void ShouldThrowOptionsLevelException()
        {
            static void action() => new WeightlessExpirableEntry()
                .SetOptions(new ExpirableEntryOptions()
                {
                    AbsouluteExpiration = DateTime.Now,
                    RelativeExpiration = TimeSpan.FromSeconds(10),
                    Size = -1
                });

            Assert.Throws<ArgumentOutOfRangeException>(action);
        }
    }
}
