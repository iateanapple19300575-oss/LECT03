Imports System.IO
Imports Framework.Core

Namespace Framework.IO

    ''' <summary>
    ''' ファイル操作を一元化するユーティリティクラス。
    ''' <para>
    ''' ・読み込み  
    ''' ・書き込み  
    ''' ・存在確認  
    ''' ・コピー／移動／削除  
    ''' ・ディレクトリ操作  
    ''' </para>
    ''' を統合し、例外処理・ログ出力・エラーメッセージを標準化する。
    ''' </summary>
    Public NotInheritable Class FileHelper

        ' ============================
        ' 読み込み
        ' ============================

        ''' <summary>
        ''' 指定されたファイルの内容をすべて読み込み、文字列として返す。
        ''' </summary>
        ''' <param name="path">読み込むファイルのパス。</param>
        ''' <returns>ファイル内容の文字列。</returns>
        ''' <exception cref="Exception">読み込み中にエラーが発生した場合。</exception>
        Public Shared Function ReadAllText(path As String) As String
            Try
                AppLogger.Info("ReadAllText: " & path)
                Return File.ReadAllText(path)

            Catch ex As Exception
                Throw New Exception(ErrorHandler.Handle(ex, "FileHelper.ReadAllText"))
            End Try
        End Function

        ''' <summary>
        ''' 指定されたファイルの内容を行単位で読み込み、文字列配列として返す。
        ''' </summary>
        ''' <param name="path">読み込むファイルのパス。</param>
        ''' <returns>ファイル内容の行配列。</returns>
        ''' <exception cref="Exception">読み込み中にエラーが発生した場合。</exception>
        Public Shared Function ReadAllLines(path As String) As String()
            Try
                AppLogger.Info("ReadAllLines: " & path)
                Return File.ReadAllLines(path)

            Catch ex As Exception
                Throw New Exception(ErrorHandler.Handle(ex, "FileHelper.ReadAllLines"))
            End Try
        End Function

        ' ============================
        ' 書き込み
        ' ============================

        ''' <summary>
        ''' 指定された文字列をファイルに書き込む。
        ''' </summary>
        ''' <param name="path">書き込み先ファイルのパス。</param>
        ''' <param name="content">書き込む文字列。</param>
        ''' <exception cref="Exception">書き込み中にエラーが発生した場合。</exception>
        Public Shared Sub WriteAllText(path As String, content As String)
            Try
                AppLogger.Info("WriteAllText: " & path)
                File.WriteAllText(path, content)

            Catch ex As Exception
                Throw New Exception(ErrorHandler.Handle(ex, "FileHelper.WriteAllText"))
            End Try
        End Sub

        ''' <summary>
        ''' 指定された文字列の列挙をファイルに行単位で書き込む。
        ''' </summary>
        ''' <param name="path">書き込み先ファイルのパス。</param>
        ''' <param name="lines">書き込む行データ。</param>
        ''' <exception cref="Exception">書き込み中にエラーが発生した場合。</exception>
        Public Shared Sub WriteAllLines(path As String, lines As IEnumerable(Of String))
            Try
                AppLogger.Info("WriteAllLines: " & path)
                File.WriteAllLines(path, lines.ToArray())

            Catch ex As Exception
                Throw New Exception(ErrorHandler.Handle(ex, "FileHelper.WriteAllLines"))
            End Try
        End Sub

        ' ============================
        ' 存在確認
        ' ============================

        ''' <summary>
        ''' 指定されたパスのファイルが存在するかどうかを返す。
        ''' </summary>
        ''' <param name="path">確認するファイルのパス。</param>
        ''' <returns>存在する場合 True。</returns>
        ''' <exception cref="Exception">確認中にエラーが発生した場合。</exception>
        Public Shared Function Exists(path As String) As Boolean
            Try
                Return File.Exists(path)

            Catch ex As Exception
                Throw New Exception(ErrorHandler.Handle(ex, "FileHelper.Exists"))
            End Try
        End Function

        ' ============================
        ' コピー
        ' ============================

        ''' <summary>
        ''' ファイルをコピーする。
        ''' </summary>
        ''' <param name="src">コピー元ファイルのパス。</param>
        ''' <param name="dest">コピー先ファイルのパス。</param>
        ''' <param name="overwrite">既存ファイルを上書きするかどうか。</param>
        ''' <exception cref="Exception">コピー中にエラーが発生した場合。</exception>
        Public Shared Sub Copy(src As String, dest As String, Optional overwrite As Boolean = False)
            Try
                AppLogger.Info(String.Format("Copy: {0} → {1}", src, dest))
                File.Copy(src, dest, overwrite)

            Catch ex As Exception
                Throw New Exception(ErrorHandler.Handle(ex, "FileHelper.Copy"))
            End Try
        End Sub

        ' ============================
        ' 移動
        ' ============================

        ''' <summary>
        ''' ファイルを移動する。
        ''' </summary>
        ''' <param name="src">移動元ファイルのパス。</param>
        ''' <param name="dest">移動先ファイルのパス。</param>
        ''' <exception cref="Exception">移動中にエラーが発生した場合。</exception>
        Public Shared Sub Move(src As String, dest As String)
            Try
                AppLogger.Info(String.Format("Move: {0} → {1}", src, dest))
                File.Move(src, dest)

            Catch ex As Exception
                Throw New Exception(ErrorHandler.Handle(ex, "FileHelper.Move"))
            End Try
        End Sub

        ' ============================
        ' 削除
        ' ============================

        ''' <summary>
        ''' 指定されたファイルを削除する。
        ''' </summary>
        ''' <param name="path">削除するファイルのパス。</param>
        ''' <exception cref="Exception">削除中にエラーが発生した場合。</exception>
        Public Shared Sub Delete(path As String)
            Try
                AppLogger.Warn("Delete: " & path)
                File.Delete(path)

            Catch ex As Exception
                Throw New Exception(ErrorHandler.Handle(ex, "FileHelper.Delete"))
            End Try
        End Sub

        ' ============================
        ' ディレクトリ操作
        ' ============================

        ''' <summary>
        ''' 指定されたパスにディレクトリを作成する。
        ''' 既に存在する場合は何もしない。
        ''' </summary>
        ''' <param name="path">作成するディレクトリのパス。</param>
        ''' <exception cref="Exception">作成中にエラーが発生した場合。</exception>
        Public Shared Sub CreateDirectory(path As String)
            Try
                AppLogger.Info("CreateDirectory: " & path)
                Directory.CreateDirectory(path)

            Catch ex As Exception
                Throw New Exception(ErrorHandler.Handle(ex, "FileHelper.CreateDirectory"))
            End Try
        End Sub

        ''' <summary>
        ''' 指定されたパスのディレクトリが存在するかどうかを返す。
        ''' </summary>
        ''' <param name="path">確認するディレクトリのパス。</param>
        ''' <returns>存在する場合 True。</returns>
        ''' <exception cref="Exception">確認中にエラーが発生した場合。</exception>
        Public Shared Function DirectoryExists(path As String) As Boolean
            Try
                Return Directory.Exists(path)

            Catch ex As Exception
                Throw New Exception(ErrorHandler.Handle(ex, "FileHelper.DirectoryExists"))
            End Try
        End Function

    End Class

End Namespace