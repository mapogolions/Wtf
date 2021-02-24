using System;

namespace OptionsResponsiblity
{
    public interface IExpirableEntry
    {
        DateTimeOffset AbsoluteExpiration { get; set; }
        TimeSpan RelativeToNowExpiration { get; set; }
        int? Size { get; set; }
    }

    public static class ExpirableEntryExtensions
    {
        public static IExpirableEntry SetOptions(this IExpirableEntry expirableEntry, ExpirableEntryOptions options)
        {
            if (options is null)
                throw new ArgumentNullException(nameof(options));
            expirableEntry.AbsoluteExpiration = options.AbsouluteExpiration;
            expirableEntry.RelativeToNowExpiration = options.RelativeExpiration;
            expirableEntry.Size = options.Size;
            return expirableEntry;
        }
    }

    public class ExpirableEntryOptions
    {
        private DateTimeOffset _absoluteExpiration;
        private TimeSpan _relativeToNowExpiration;
        private int? _size;

        public DateTimeOffset AbsouluteExpiration
        {
            get => _absoluteExpiration;
            set => _absoluteExpiration = value;
        }

        public TimeSpan RelativeExpiration
        {
            get => _relativeToNowExpiration;
            set
            {
                if (value <= TimeSpan.Zero)
                    throw new ArgumentOutOfRangeException(
                        nameof(RelativeExpiration),
                        value,
                        "The relative expiration value must be positive.");
                _relativeToNowExpiration = value;
            }
        }

        public int? Size
        {
            get => _size;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), value, $"{nameof(value)} must be non-negative.");
                _size = value;
            }
        }
    }

    public class WeightlessExpirableEntry : IExpirableEntry
    {
        private readonly int? _size = 0;
        private DateTimeOffset _absoluteExpiration;
        private TimeSpan _relativeToNowExpiration;

        public DateTimeOffset AbsoluteExpiration
        {
            get => _absoluteExpiration;
            set => _absoluteExpiration = value;
        }

        public TimeSpan RelativeToNowExpiration
        {
            get => _relativeToNowExpiration;
            set
            {
                // duplicate ExpirableEntryOptions validaiton logic
                if (value <= TimeSpan.Zero)
                    throw new ArgumentOutOfRangeException(
                        nameof(RelativeToNowExpiration),
                        value,
                        "The relative expiration value must be greater than zero.");
                _relativeToNowExpiration = value;
            }
        }

        public int? Size
        {
            get => _size;
            set => throw new InvalidOperationException();
        }
    }
}
