﻿Imports System.Collections.Generic
Imports System.Security.Claims
Imports System.Security.Principal
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls

Public Partial Class SiteMaster
    Inherits MasterPage
    Private Const AntiXsrfTokenKey As String = "__AntiXsrfToken"
    Private Const AntiXsrfUserNameKey As String = "__AntiXsrfUserName"
    Private _antiXsrfTokenValue As String

    Protected Sub Page_Init(sender As Object, e As EventArgs)
        ' 以下のコードは、XSRF 攻撃からの保護に役立ちます
        Dim requestCookie = Request.Cookies(AntiXsrfTokenKey)
        Dim requestCookieGuidValue As Guid
        If requestCookie IsNot Nothing AndAlso Guid.TryParse(requestCookie.Value, requestCookieGuidValue) Then
            ' Cookie の Anti-XSRF トークンを使用します
            _antiXsrfTokenValue = requestCookie.Value
            Page.ViewStateUserKey = _antiXsrfTokenValue
        Else
            ' 新しい Anti-XSRF トークンを生成し、Cookie に保存します
            _antiXsrfTokenValue = Guid.NewGuid().ToString("N")
            Page.ViewStateUserKey = _antiXsrfTokenValue

            Dim responseCookie = New HttpCookie(AntiXsrfTokenKey) With {
                .HttpOnly = True,
                .Value = _antiXsrfTokenValue
            }
            If FormsAuthentication.RequireSSL AndAlso Request.IsSecureConnection Then
                responseCookie.Secure = True
            End If
            Response.Cookies.[Set](responseCookie)
        End If

        AddHandler Page.PreLoad, AddressOf master_Page_PreLoad
    End Sub

    Protected Sub master_Page_PreLoad(sender As Object, e As EventArgs)
        If Not IsPostBack Then
            ' Anti-XSRF トークンを設定します
            ViewState(AntiXsrfTokenKey) = Page.ViewStateUserKey
            ViewState(AntiXsrfUserNameKey) = If(Context.User.Identity.Name, [String].Empty)
        Else
            ' Anti-XSRF トークンを検証します
            If DirectCast(ViewState(AntiXsrfTokenKey), String) <> _antiXsrfTokenValue OrElse DirectCast(ViewState(AntiXsrfUserNameKey), String) <> (If(Context.User.Identity.Name, [String].Empty)) Then
                Throw New InvalidOperationException("Anti-XSRF トークンの検証が失敗しました。")
            End If
        End If
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs)

    End Sub

    Protected Sub Unnamed_LoggingOut(sender As Object, e As LoginCancelEventArgs)
        Context.GetOwinContext().Authentication.SignOut()
    End Sub
End Class
