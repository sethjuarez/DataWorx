using Xunit;
using System;

namespace DataWorx.Core.Tests
{
    [Trait("Category", "General")]
    public class FillTests
    {
        [Theory, InlineData(typeof(DayOfWeek))]
        public void GetDatum(Type t)
        {
            if(t.IsEnum)
            {
                var values = Enum.GetValues(t);
                foreach (var item in values)
                {
                    var s = string.Format("{0} - {1}", (int)item, Enum.GetName(t, item));
                }
            }
            
        }
    }
}
