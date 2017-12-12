namespace VigenereGui.Vigenere.Algorithms
{
    public interface IAlgorithm
    {
        string FindKey(int keyLength, string text);
    }
}
