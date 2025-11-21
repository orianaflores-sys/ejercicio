namespace ProyectoTecWeb.Models.DTO
{
    public class TokenPresenterDto
    {
        public required string AcessToken { get; set;  }
        
        public required string TokenType { get; set; }
        
        public int? ExpireIn { get; set;  }
    }
}