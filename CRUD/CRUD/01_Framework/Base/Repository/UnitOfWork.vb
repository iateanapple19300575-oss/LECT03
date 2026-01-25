Imports System.Data.SqlClient

Public Class UnitOfWork
    Implements IDisposable

    Private ReadOnly _connection As SqlConnection
    Private ReadOnly _transaction As SqlTransaction
    Private _committed As Boolean = False

    Public Sub New()
        _connection = ConnectionFactory.Create()
        _transaction = _connection.BeginTransaction()
    End Sub

    Public ReadOnly Property Connection As SqlConnection
        Get
            Return _connection
        End Get
    End Property

    Public ReadOnly Property Transaction As SqlTransaction
        Get
            Return _transaction
        End Get
    End Property

    Public Sub Commit()
        _transaction.Commit()
        _committed = True
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        If Not _committed Then
            Try
                _transaction.Rollback()
            Catch
            End Try
        End If
        _transaction.Dispose()
        _connection.Dispose()
    End Sub

End Class