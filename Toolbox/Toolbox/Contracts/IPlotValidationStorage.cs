using System;
using System.Collections.Generic;
using Toolbox.Logic;

namespace Toolbox.Contracts
{
    public interface IPlotValidationStorage
    {
        public bool checkIMEIRange();
        public bool checkDateRange();
        public bool checkMissingRange();
        public bool checkTotalPlotsReviewedRange();
    }
}
