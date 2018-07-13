using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;

public class ClientObject
{
    private HttpListenerContext context;
    
    public ClientObject(HttpListenerContext context)
    {
        this.context = context;
    }

    public void SendResponse(string message, string errMessage, BinaryWriter writer, BinaryReader reader)
    {
        Console.WriteLine(message + "\nВозможные ошибки:" + errMessage);
        writer.Write(message + "\nВозможные ошибки:" + errMessage);
        writer.Flush();
        reader.Close();
        writer.Close();
    }

    public void Process()
    {
        HttpListenerRequest request = context.Request;
        HttpListenerResponse response = context.Response;

        HttpListenerRequest req = context.Request;
        Stream streamBody = request.InputStream;
        Encoding encoding = req.ContentEncoding;
        StreamReader streamReader = new StreamReader(streamBody, encoding);
        string sRequest = streamReader.ReadToEnd();


        string responseString = $"{request.HttpMethod}";
        Console.WriteLine($"{request.HttpMethod} {sRequest}");
        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
        response.ContentLength64 = buffer.Length;
        Stream output = response.OutputStream;
        output.Write(buffer, 0, buffer.Length);
        output.Close();
    }
}
