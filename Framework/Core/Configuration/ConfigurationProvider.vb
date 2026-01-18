Imports System.Configuration
Imports Framework.Core

Namespace Framework.Configuration

    ''' <summary>
    ''' App.config の設定値を一元管理するクラス。
    ''' 文字列・整数・Boolean の型変換、例外処理、エラーログ統合を行う。
    ''' </summary>
    Public NotInheritable Class ConfigurationProvider

        ' ============================
        ' 基本的な取得処理
        ' ============================

        ''' <summary>
        ''' App.config の AppSettings から文字列値を取得する。
        ''' キーが存在しない場合は例外を発生させる。
        ''' </summary>
        ''' <param name="key">設定キー。</param>
        ''' <returns>設定値の文字列。</returns>
        ''' <exception cref="Exception">キーが存在しない場合、または内部エラーが発生した場合。</exception>
        Private Shared Function GetString(key As String) As String
            Try
                Dim value = ConfigurationManager.AppSettings(key)

                If value Is Nothing Then
                    Throw New Exception("設定キーが存在しません: " & key)
                End If

                Return value

            Catch ex As Exception
                Throw New Exception(ErrorHandler.Handle(ex, "ConfigurationProvider.GetString"))
            End Try
        End Function

        ''' <summary>
        ''' AppSettings から整数値を取得する。
        ''' 値が整数に変換できない場合は例外を発生させる。
        ''' </summary>
        ''' <param name="key">設定キー。</param>
        ''' <returns>整数値。</returns>
        ''' <exception cref="Exception">整数に変換できない場合。</exception>
        Private Shared Function GetInt(key As String) As Integer
            Dim s = GetString(key)
            Dim num As Integer

            If Not Integer.TryParse(s, num) Then
                Throw New Exception("設定値が整数ではありません: " & key)
            End If

            Return num
        End Function

        ''' <summary>
        ''' AppSettings から Boolean 値を取得する。
        ''' 値が Boolean に変換できない場合は例外を発生させる。
        ''' </summary>
        ''' <param name="key">設定キー。</param>
        ''' <returns>Boolean 値。</returns>
        ''' <exception cref="Exception">Boolean に変換できない場合。</exception>
        Private Shared Function GetBool(key As String) As Boolean
            Dim s = GetString(key)
            Dim b As Boolean

            If Not Boolean.TryParse(s, b) Then
                Throw New Exception("設定値が Boolean ではありません: " & key)
            End If

            Return b
        End Function

        ' ============================
        ' 公開プロパティ（ここに集約）
        ' ============================

        ''' <summary>
        ''' データベース接続文字列。
        ''' </summary>
        Public Shared ReadOnly Property ConnectionString As String
            Get
                Return GetString("ConnectionString")
            End Get
        End Property

        ''' <summary>
        ''' CSV ファイルの配置フォルダパス。
        ''' </summary>
        Public Shared ReadOnly Property CsvFolder As String
            Get
                Return GetString("CsvFolder")
            End Get
        End Property

        ''' <summary>
        ''' エクスポートファイルの出力フォルダパス。
        ''' </summary>
        Public Shared ReadOnly Property ExportFolder As String
            Get
                Return GetString("ExportFolder")
            End Get
        End Property

        ''' <summary>
        ''' リトライ回数の最大値。
        ''' </summary>
        Public Shared ReadOnly Property MaxRetryCount As Integer
            Get
                Return GetInt("MaxRetryCount")
            End Get
        End Property

        ''' <summary>
        ''' デバッグモードの有効・無効。
        ''' </summary>
        Public Shared ReadOnly Property EnableDebugMode As Boolean
            Get
                Return GetBool("EnableDebugMode")
            End Get
        End Property

    End Class

End Namespace