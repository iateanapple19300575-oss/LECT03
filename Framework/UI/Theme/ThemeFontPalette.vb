Namespace Framework.UI.Theme

    ''' <summary>
    ''' アプリケーション全体で使用するフォント設定をまとめたパレットクラス。
    ''' <para>
    ''' ・標準フォント  
    ''' ・タイトル用フォント  
    ''' ・ボタン用フォント  
    ''' </para>
    ''' UI の統一感を保ち、テーマ変更を容易にするための中心的な設定クラス。
    ''' </summary>
    Public Class ThemeFontPalette

        ''' <summary>
        ''' 画面全体で使用される標準フォント。
        ''' </summary>
        Public Property DefaultFont As Font = New Font("Meiryo UI", 9.0F)

        ''' <summary>
        ''' タイトルや見出しに使用されるフォント。
        ''' </summary>
        Public Property TitleFont As Font = New Font("Meiryo UI", 11.0F, FontStyle.Bold)

        ''' <summary>
        ''' ボタンに使用されるフォント。
        ''' </summary>
        Public Property ButtonFont As Font = New Font("Meiryo UI", 9.0F, FontStyle.Bold)

    End Class

End Namespace