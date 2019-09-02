using System;
using System.Collections.Generic;

namespace Warehouse.Web.Models.Components
{
    public class GenericViewObjectModel
    {
        public object Object { get; set; }

        /// <value>
        /// A list of string interpretations of Properties to be shown in the view.
        /// </value>
        /// <note>
        /// Inner properties' names will be separated by a dot '.' delimiter.
        /// </note>
        public List<string> PropsToShow { get; set; }

        /// <value>
        /// A list of string interpretations of Properties to be hidden in the view inputs.
        /// </value>
        /// <note>
        /// Inner properties' names will be separated by a dot '.' delimiter.
        /// </note>
        public List<string> HiddenProps { get; set; }
    }
}
