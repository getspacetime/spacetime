using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spacetime.Shared.Components.Containers.Alert
{
    /// <summary>
    /// Alert callback arguments
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AlertCallbackArgs<T>
    {
        public AlertCallbackArgs(bool confirm)
        {
            Confirm = confirm;
        }

        public AlertCallbackArgs(bool confirm, T data)
        {
            Confirm = confirm;
            Data = data;
        }

        /// <summary>
        /// Indicates if the user confirmed or canceled the alert.
        /// </summary>
        public bool Confirm { get; set; }

        /// <summary>
        /// Optional data passed along to the alert dialog. Used to make the
        /// request flow easier and not have to manage an internal state of 
        /// which object is being tracked, e.g. when confirming the deletion of 
        /// an item.
        /// </summary>
        public T Data { get; set; }
    }
}
