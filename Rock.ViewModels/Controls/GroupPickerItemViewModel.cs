using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Rock.ViewModels.Controls
{
        /// <summary>
        /// A view model for a single group item to be used in a picker.
        /// </summary>
        [DataContract]
        public class GroupPickerItemViewModel
        {
            [DataMember(Name = "value")]
            public Guid Value { get; set; }

            [DataMember(Name = "text")]
            public string Text { get; set; }
        }
}
