using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Azure.Core;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;


namespace DataAccess
{
    public class AzureSqlAuthenticationInterceptor : DbConnectionInterceptor
    {

        private readonly TokenCredential _tokenCredential;
        public AzureSqlAuthenticationInterceptor(TokenCredential tokenCredential)
        {
            _tokenCredential = tokenCredential ?? throw new ArgumentNullException(nameof(tokenCredential));
        }
        public override async ValueTask<InterceptionResult> ConnectionOpeningAsync(
            DbConnection connection,
            ConnectionEventData eventData,
            InterceptionResult result,
            CancellationToken cancellationToken = default)
        {
            if (connection is SqlConnection sqlConnection)
            {
                var accessToken = await _tokenCredential.GetTokenAsync(
                    new TokenRequestContext(new[] { "https://database.windows.net/.default" }),
                cancellationToken);

                sqlConnection.AccessToken = accessToken.Token;
            }

            return result;
        }

    }
}
