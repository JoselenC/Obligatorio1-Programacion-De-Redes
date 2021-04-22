namespace Common
{
    public class ProtocolSpecification
    {
        
        /// <summary>
        /// XXXX YYYY
        /// XXXX-> Largo del nombre del archivo, YYYY -> Largo del file
        /// ZZZZZZZZZZZ... -> Nombre del archivo
        /// N Segmentos (YYYY / MaxPacketSize) -> Cada Segmento mide MaxPacketSize o menos
        ///
        /// Crear nuevo post
        /// REQ05XXXX <Titulo de Post>#<Contenido del post>
        ///
        /// Listar ususarios conectados
        /// REQ010000
        ///  
        /// </summary>
          
        public const int FileNameLength = 4;
        public const int FileSizeLength = 4;
        public const int MaxPacketSize = 32768;
    }
}