using System;
using System.Collections.Generic;
using System.Text;

namespace VilliInput.Touch.Engines
{
    public class Win7Engine : TouchEngine
    {

        public Win7Engine() : base(TouchEngines.Win7) { }

        public override void Setup()
        {
            throw new NotImplementedException();

            /*
            if (System.Environment.OSVersion.Version.Major > 6 || (System.Environment.OSVersion.Version.Major == 6 && System.Environment.OSVersion.Version.Minor > 0))
            {
                // register the program window to handle touch events...
                touchHandler = Factory.CreateHandler<TouchHandler>(Villi.Game.Window.Handle);
                // pressing...
                touchHandler.TouchDown += touchHandler_TouchDown;
                // pressing done...
                touchHandler.TouchUp += touchHandler_TouchUp;
                // moving...
                touchHandler.TouchMove += touchHandler_TouchMove;
            }
            */
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }

    }
}
