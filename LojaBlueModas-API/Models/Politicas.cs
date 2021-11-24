using Microsoft.AspNetCore.Authorization;

namespace LojaBlueModas_API.Models
{
    public partial class Politicas
    {
        public static AuthorizationPolicy AdminPolitica()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser()
                                                   .RequireRole(UsuarioRegra.Admin)
                                                   .Build();
        }

        public static AuthorizationPolicy UsuarioPolitica()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser()
                                                   .RequireRole(UsuarioRegra.Usuario)
                                                   .Build();
        }
    }
}
