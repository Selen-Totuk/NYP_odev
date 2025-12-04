using System;

public class KuantumCokusuException : Exception
{
    public string PatlayanNesneID { get; private set; }

    // Hata fırlatıldığında patlayan nesnenin ID'sini mesajda gösterecek constructor.
    public KuantumCokusuException(string nesneId)
        : base($"Kuantum Çöküşü! Patlayan Nesne ID: {nesneId}")
    {
        PatlayanNesneID = nesneId;
    }
}