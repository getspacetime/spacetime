using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spacetime.Shared.Components.Inputs
{
    public enum ButtonType
    {
        /// <summary>
        /// Default button: ole reliable, plain, doesn't stand out. One could even venture
        /// to say it is boring.
        /// </summary>
        Default,

        /// <summary>
        /// Primary button: usually a button that should draw focus on the page.
        /// Note: if every button is a primary button, you're probably doing something
        /// wrong.
        /// </summary>
        Primary,

        /// <summary>
        /// Danger button: the side effects of this action are irreversible
        /// and should be considered carefully before executing.
        /// </summary>
        Danger,


        /// <summary>
        /// Link button: this button looks like a link (not Zelda Link).
        /// </summary>
        Link,
    }

    public enum ButtonSize
    {
        Medium,
        Small,
        Large
    }
}
