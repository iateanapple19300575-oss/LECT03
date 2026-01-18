Imports System.IO
Imports System.Text

Namespace Framework.Core

    ''' <summary>
    ''' アプリケーション全体で使用するスレッドセーフなロギングクラス。
    ''' ログレベル、ログローテーション、呼び出し元メソッド名の自動取得に対応する。
    ''' </summary>
    Public NotInheritable Class AppLogger

        ''' <summary>
        ''' ログレベルを表す列挙体。
        ''' </summary>
        Public Enum LogLevel
            Debug
            Info
            Warn
            [Error]
            Fatal
        End Enum

        ''' <summary>
        ''' ログファイルを出力するディレクトリ。
        ''' </summary>
        Private Shared _logDirectory As String = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs")

        ''' <summary>
        ''' ログファイル名。
        ''' </summary>
        Private Shared _logFileName As String = "application.log"

        ''' <summary>
        ''' スレッドセーフな書き込みを保証するためのロックオブジェクト。
        ''' </summary>
        Private Shared ReadOnly _lockObj As New Object()

        ''' <summary>
        ''' ログローテーションを行う最大ファイルサイズ（5MB）。
        ''' </summary>
        Private Const MAX_LOG_SIZE As Integer = 5 * 1024 * 1024

        ''' <summary>
        ''' ロガーを初期化する。
        ''' ログディレクトリやファイル名を変更したい場合に使用する。
        ''' </summary>
        ''' <param name="logDir">ログディレクトリのパス。</param>
        ''' <param name="fileName">ログファイル名。</param>
        Public Shared Sub Initialize(Optional ByVal logDir As String = Nothing, Optional ByVal fileName As String = Nothing)
            If Not String.IsNullOrEmpty(logDir) Then _logDirectory = logDir
            If Not String.IsNullOrEmpty(fileName) Then _logFileName = fileName

            If Not Directory.Exists(_logDirectory) Then
                Directory.CreateDirectory(_logDirectory)
            End If
        End Sub

        ''' <summary>
        ''' Debug レベルのログを出力する。
        ''' </summary>
        Public Shared Sub Debug(message As String, Optional memberName As String = Nothing)
            WriteLog(LogLevel.Debug, message, memberName)
        End Sub

        ''' <summary>
        ''' Info レベルのログを出力する。
        ''' </summary>
        Public Shared Sub Info(message As String, Optional memberName As String = Nothing)
            WriteLog(LogLevel.Info, message, memberName)
        End Sub

        ''' <summary>
        ''' Warn レベルのログを出力する。
        ''' </summary>
        Public Shared Sub Warn(message As String, Optional memberName As String = Nothing)
            WriteLog(LogLevel.Warn, message, memberName)
        End Sub

        ''' <summary>
        ''' Error レベルのログを出力する。
        ''' </summary>
        Public Shared Sub [Error](message As String, Optional memberName As String = Nothing)
            WriteLog(LogLevel.Error, message, memberName)
        End Sub

        ''' <summary>
        ''' Fatal レベルのログを出力する。
        ''' </summary>
        Public Shared Sub Fatal(message As String, Optional memberName As String = Nothing)
            WriteLog(LogLevel.Fatal, message, memberName)
        End Sub

        ''' <summary>
        ''' 例外情報をログとして出力する。
        ''' メッセージ、スタックトレース、内部例外を含む。
        ''' </summary>
        ''' <param name="ex">ログ出力する例外。</param>
        ''' <param name="memberName">呼び出し元メソッド名。</param>
        Public Shared Sub LogException(ex As Exception, Optional memberName As String = Nothing)
            Dim msg As New StringBuilder()
            msg.AppendLine("Exception Message: " & ex.Message)
            msg.AppendLine("StackTrace: " & ex.StackTrace)

            If ex.InnerException IsNot Nothing Then
                msg.AppendLine("InnerException: " & ex.InnerException.Message)
            End If

            WriteLog(LogLevel.Error, msg.ToString(), memberName)
        End Sub

        ''' <summary>
        ''' UI 操作ログを出力する。
        ''' 操作名、コントロール名、値を記録する。
        ''' </summary>
        Public Shared Sub UiAction(action As String, controlName As String, value As String, Optional memberName As String = Nothing)
            Dim msg As String = String.Format("UI Action: {0}, Control: {1}, Value: {2}", action, controlName, value)
            WriteLog(LogLevel.Info, msg, memberName)
        End Sub

        ''' <summary>
        ''' ログを書き込む内部メソッド。
        ''' ログローテーション、呼び出し元取得、スレッドセーフ処理を行う。
        ''' </summary>
        Private Shared Sub WriteLog(level As LogLevel, message As String, memberName As String)
            SyncLock _lockObj
                Try
                    Dim filePath As String = Path.Combine(_logDirectory, _logFileName)

                    RotateIfNeeded(filePath)

                    If String.IsNullOrEmpty(memberName) Then
                        memberName = GetCallerName()
                    End If

                    Dim logLine As String = String.Format(
                        "{0:yyyy-MM-dd HH:mm:ss.fff} [{1}] ({2}) {3}",
                        DateTime.Now,
                        level.ToString(),
                        memberName,
                        message.Replace(vbCrLf, " ")
                    )

                    Using sw As New StreamWriter(filePath, True, Encoding.UTF8)
                        sw.WriteLine(logLine)
                    End Using

                Catch ex As Exception
                    ' ログ書き込み失敗時は何もしない
                End Try
            End SyncLock
        End Sub

        ''' <summary>
        ''' ログファイルが最大サイズを超えている場合、バックアップしてローテーションする。
        ''' </summary>
        Private Shared Sub RotateIfNeeded(filePath As String)
            If Not File.Exists(filePath) Then Exit Sub

            Dim fileInfo As New FileInfo(filePath)
            If fileInfo.Length < MAX_LOG_SIZE Then Exit Sub

            Dim backupName As String = String.Format(
                "{0}_{1:yyyyMMdd_HHmmss}.log",
                Path.GetFileNameWithoutExtension(_logFileName),
                DateTime.Now
            )

            Dim backupPath As String = Path.Combine(_logDirectory, backupName)

            File.Move(filePath, backupPath)
        End Sub

        ''' <summary>
        ''' 呼び出し元メソッド名をスタックトレースから取得する。
        ''' </summary>
        ''' <returns>呼び出し元メソッド名。</returns>
        Private Shared Function GetCallerName() As String
            Dim st As New Diagnostics.StackTrace()
            If st.FrameCount >= 3 Then
                Return st.GetFrame(2).GetMethod().Name
            End If
            Return "Unknown"
        End Function

    End Class

End Namespace