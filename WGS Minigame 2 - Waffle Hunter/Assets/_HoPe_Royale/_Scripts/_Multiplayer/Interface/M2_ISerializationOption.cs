using M2_SimpleJSON;

namespace RoyaleMinigames.Interface
{
    public interface M2_ISerializationOption
    {
        string ContentType { get; }
        T Deserialize<T>(string text);

        JSONNode Deserialize(string text);
    }
}
