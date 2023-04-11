using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baba.Particles.Decorators
{
    public abstract class EmissionDecorator
    {
        internal abstract void Apply(Particle particle);
    }
}
