using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Script.GRLO
{
    public interface IState
    {
        public string StateName { get; set; }
        void Tick();
        void OnEnter();
        void OnExit();
    }
}
