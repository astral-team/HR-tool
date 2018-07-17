﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using httpListener;
using RestSharp;
using RestSharp.Authenticators;

public class ClientLogin
{
    private HttpListenerContext context;
    


    public ClientLogin(HttpListenerContext context)
    {
        this.context = context;
    }

    public void SendResponse(string message, string errMessage, BinaryWriter writer, BinaryReader reader)
    {
        Console.WriteLine(message + "\nВозможные ошибки:" + errMessage);
        writer.Write(message + "\nВозможные ошибки:" + errMessage);
       
    }

    public void Process()
    {
        HttpListenerRequest request = context.Request;
        HttpListenerResponse response = context.Response;
        


        Stream streamBody = request.InputStream;
        Encoding encoding = request.ContentEncoding;
        //BinaryReader breader = new BinaryReader(streamBody, encoding);
        StreamReader streamReader = new StreamReader(streamBody, encoding);
        var sRequest = streamReader.ReadToEnd();

        AuthorizedUser user = new AuthorizedUser(request.Headers["login"], request.Headers["Authorization"]);
        //Logins userDb = null;
        /*try
        {
            userDb = CRUD.GetUser(user);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }*/
        var userDb = CRUD.GetUser(user);
        string responseString = "";

        if (userDb != null)
        {
            user.Id = userDb.Id;
            var sessionDb = CRUD.GetSession(user);
            sessionDb.SessionKey = user.GetSessionKey();
            CRUD.SetSession(sessionDb);
            responseString = $"Пользователь вошел Логин={user.Login}, Hash={user.Hash}, Session={user.SessionKey}";
        }
        else
        {
            responseString = $"Ошибка авторизации Логин={user.Login}, Hash={user.Hash}";
        }

        Console.WriteLine(responseString);


        //Console.WriteLine($"{request.HttpMethod} {sRequest} {request.Headers} {request.UserAgent}");
        
        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
        response.ContentLength64 = buffer.Length;
        Stream output = response.OutputStream;
        output.Write(buffer, 0, buffer.Length);
        output.Close();
    }
}
