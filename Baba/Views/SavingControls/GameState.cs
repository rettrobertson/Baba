using System;
using Microsoft.Xna.Framework.Input;

namespace Baba.Views.SavingControls
{



    public class GameState
    {
        /// <summary>
        /// Have to have a default constructor for the XmlSerializer.Deserialize method
        /// </summary>
        public GameState() { }

        /// <summary>
        /// Overloaded constructor used to create an object for long term storage
        /// </summary>
        /// <param name="controls"></param>

        public GameState(Keys[] controls)
        {
            Controls = controls;

        }


        public Keys[] Controls { get; set; }
    }

}
