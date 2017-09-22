Imports Owin
Imports Microsoft.Owin.Security.Cookies
Imports Microsoft.Owin.Security.Google
Imports Microsoft.Owin

Partial Public Class Startup
    ' 認証の構成の詳細については、http://go.microsoft.com/fwlink/?LinkId=301883 を参照してください
    Public Sub ConfigureAuth(app As IAppBuilder)
        ' サインインしたユーザーの情報を保存するために、アプリケーションが Cookie を使用できるようにします
        ' また、サード パーティ ログイン プロバイダーを使用してログインしたユーザーに関する情報を保存します。
        ' アプリケーションがユーザーのログインを許可している場合、これは必須です
        app.UseCookieAuthentication(New CookieAuthenticationOptions() With {
        .AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
        .LoginPath = New PathString("/Account/Login")})
        app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie)

        ' 次の行のコメントを解除して、サード パーティ ログイン プロバイダーを使用したログインを有効にします
        'app.UseMicrosoftAccountAuthentication(
        '    clientId:= "",
        '    clientSecret:= "")

        'app.UseTwitterAuthentication(
        '   consumerKey:= "",
        '   consumerSecret:= "")

        'app.UseFacebookAuthentication(
        '   appId:= "",
        '   appSecret:= "")

        'app.UseGoogleAuthentication(New GoogleOAuth2AuthenticationOptions() With {
        '   .ClientId = "",
        '   .ClientSecret = ""})
    End Sub
End Class
