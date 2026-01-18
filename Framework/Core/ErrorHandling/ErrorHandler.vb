Imports Framework.Validation

Namespace Framework.Core

    ''' <summary>
    ''' 例外処理を一元化するクラス。
    ''' 例外分類、ユーザー向けメッセージ生成、ログ出力を担当する。
    ''' </summary>
    Public NotInheritable Class ErrorHandler

        ''' <summary>
        ''' 例外の種類を分類するための列挙体。
        ''' </summary>
        Public Enum ErrorCategory
            Database
            IO
            Validation
            Business
            Unexpected
        End Enum

        ''' <summary>
        ''' 例外を処理し、ユーザー向けメッセージを返す。
        ''' ログ出力も内部で行う。
        ''' </summary>
        Public Shared Function Handle(ex As Exception, Optional memberName As String = Nothing) As String

            ' 例外分類
            Dim category As ErrorCategory = ClassifyException(ex)

            ' ログ出力
            AppLogger.LogException(ex, memberName)

            ' ユーザー向けメッセージ生成
            Return BuildUserMessage(category, ex)
        End Function

        ''' <summary>
        ''' 例外を分類する。
        ''' </summary>
        Private Shared Function ClassifyException(ex As Exception) As ErrorCategory

            ' DB 関連
            If TypeOf ex Is System.Data.SqlClient.SqlException Then
                Return ErrorCategory.Database
            End If

            ' IO 関連
            If TypeOf ex Is System.IO.IOException Then
                Return ErrorCategory.IO
            End If

            ' Validation（業務ロジックで投げる想定）
            If TypeOf ex Is ValidationException Then
                Return ErrorCategory.Validation
            End If

            ' Business（業務例外）
            If TypeOf ex Is BusinessException Then
                Return ErrorCategory.Business
            End If

            ' その他
            Return ErrorCategory.Unexpected
        End Function

        ''' <summary>
        ''' ユーザー向けメッセージを生成する。
        ''' </summary>
        Private Shared Function BuildUserMessage(category As ErrorCategory, ex As Exception) As String

            Select Case category

                Case ErrorCategory.Database
                    Return "データベース処理中にエラーが発生しました。時間をおいて再度実行してください。"

                Case ErrorCategory.IO
                    Return "ファイルまたはフォルダへのアクセス中にエラーが発生しました。権限やファイル状態を確認してください。"

                Case ErrorCategory.Validation
                    ' ValidationException はメッセージをそのまま UI に返す
                    Return ex.Message

                Case ErrorCategory.Business
                    ' 業務例外もメッセージをそのまま返す
                    Return ex.Message

                Case Else
                    Return "予期しないエラーが発生しました。システム管理者へ連絡してください。"

            End Select

        End Function

    End Class

    '''' <summary>
    '''' 業務ロジックで使用するバリデーション例外。
    '''' </summary>
    'Public Class ValidationException
    '    Inherits ApplicationException

    '    Public Sub New(message As String)
    '        MyBase.New(message)
    '    End Sub

    'End Class

    ''' <summary>
    ''' 業務ロジックで使用するビジネス例外。
    ''' </summary>
    Public Class BusinessException
        Inherits ApplicationException

        Public Sub New(message As String)
            MyBase.New(message)
        End Sub

    End Class

End Namespace