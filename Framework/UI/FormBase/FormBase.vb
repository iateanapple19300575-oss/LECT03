Imports System.Windows.Forms
Imports Framework.Core

Namespace Framework.UI

    ''' <summary>
    ''' すべてのフォームが継承する基底クラス。
    ''' <para>
    ''' ・イベント登録の一元化（<see cref="EventWrapper"/>）  
    ''' ・例外処理の統一（<see cref="SafeExecute"/>）  
    ''' ・ログ出力や初期化処理の標準化  
    ''' </para>
    ''' アプリケーション全体で統一された UI 振る舞いを提供するための基盤となる。
    ''' </summary>
    Public Class FormBase
        Inherits Form

        ''' <summary>
        ''' フォーム読み込み時の共通処理。
        ''' イベント登録および派生クラスの初期化処理を実行する。
        ''' </summary>
        ''' <param name="e">イベントデータ。</param>
        Protected Overrides Sub OnLoad(e As EventArgs)
            MyBase.OnLoad(e)

            Try
                ' UI イベントの一元登録
                EventWrapper.AttachHandlers(Me)

                ' 派生クラス固有の初期化処理
                InitializeForm()

            Catch ex As Exception
                Dim msg = ErrorHandler.Handle(ex, "FormBase.OnLoad")
                MessageBox.Show(msg)
            End Try
        End Sub

        ''' <summary>
        ''' 派生フォームが初期化処理を実装するためのテンプレートメソッド。
        ''' 必要に応じてオーバーライドして使用する。
        ''' </summary>
        Protected Overridable Sub InitializeForm()
            ' 派生クラスで実装
        End Sub

        ''' <summary>
        ''' 共通の例外処理付き実行メソッド。
        ''' <para>
        ''' ・任意の処理を安全に実行  
        ''' ・例外発生時はログ出力し、ユーザーにメッセージを表示  
        ''' </para>
        ''' </summary>
        ''' <param name="action">実行する処理。</param>
        Protected Sub SafeExecute(action As Action)
            Try
                action.Invoke()
            Catch ex As Exception
                Dim msg = ErrorHandler.Handle(ex, "FormBase.SafeExecute")
                MessageBox.Show(msg)
            End Try
        End Sub

        ''' <summary>
        ''' フォームにアクセスするために必要な権限を返す。
        ''' <para>
        ''' ・権限チェックを行うフォームでオーバーライドして使用  
        ''' ・デフォルトは Nothing（権限不要）  
        ''' </para>
        ''' </summary>
        ''' <returns>必要な権限名。不要な場合は Nothing。</returns>
        Protected Overridable Function RequiredPermission() As String
            Return Nothing
        End Function

    End Class

End Namespace