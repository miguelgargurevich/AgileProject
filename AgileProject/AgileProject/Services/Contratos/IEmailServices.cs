using AgileProject.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgileProject.Services.Contratos
{
    public interface IEmailServices : IDisposable
    {
        void EnviarEmailCopiaOculta();
    }
}
