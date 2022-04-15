namespace Database
{
    public class TokensPair
    {
        public TokensPair(string token, string refreshToken)
        {
            Token = token;
            RefreshToken = refreshToken;
        }
        public TokensPair()
        {

        }

        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
