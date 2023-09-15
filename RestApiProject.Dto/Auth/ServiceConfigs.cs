using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Dto.Auth
{
    public class ServiceConfigs
    {
        public JwtSettings JwtSettings { get; set; } = new JwtSettings();
        
    }
    public class JwtSettings
    {
        public string Secret { get; set; } = string.Empty;

        public TimeSpan TokenLifetime { get; set; }
    }
}
