public class RegisterRequest
{
    public string Nombre { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string? Tipo { get; set; } // Opcional, por defecto será "Estudiante"
    public string? ImagenBase64 { get; set; } // Opcional, en formato base64
}
