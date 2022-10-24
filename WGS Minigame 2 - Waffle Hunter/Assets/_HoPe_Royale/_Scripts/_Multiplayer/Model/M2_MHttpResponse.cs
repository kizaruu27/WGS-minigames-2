namespace RunMinigames.Models.Http
{
    public struct M2_MHttpResponse<T>
    {
        public T response { get; private set; }
        public bool isLoading { get; private set; }
        public bool isSuccess { get; private set; }
        public float requestProgress { get; private set; }

        public M2_MHttpResponse(T model, bool loading, bool success, float progress)
        {
            response = model;
            isLoading = loading;
            isSuccess = success;
            requestProgress = progress;
        }
    }
}