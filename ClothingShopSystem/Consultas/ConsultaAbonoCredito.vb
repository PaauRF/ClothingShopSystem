﻿Imports System.Data.SqlClient
Public Class ConsultaAbonoCredito
    Dim connection = openConnection()
    Dim command As SqlCommand = connection.CreateCommand()
    Dim lector As SqlDataReader

    Private Sub ConsultaAbonoCredito_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        connection.open
        cbCliente.Items.Clear()
        command.CommandText = "SELECT nombre FROM Clientes where idCliente > 1"
        lector = command.ExecuteReader

        While lector.Read
            cbCliente.Items.Add(lector(0))
        End While
        lector.Close()
    End Sub

    Private Sub cbCliente_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbCliente.SelectedIndexChanged
        cbVentas.Items.Clear()
        dgAgregar.Rows.Clear()
        command.CommandText = "SELECT idCliente, telefono, domicilio, saldo from Clientes where nombre = '" & cbCliente.Text & "'"
        lector = command.ExecuteReader
        lector.Read()
        txtIdCliente.Text = lector(0)
        txtTelefono.Text = lector(1)
        txtDomicilio.Text = lector(2)
        txtSaldo.Text = lector(3)
        lector.Close()
        cbVentas.Visible = True
        lblVentas.Visible = True

        command.CommandText = "SELECT AbonosCreditos.idAbonoC, AbonosCreditos.idVenta, AbonosCreditos.fecha, AbonosCreditos.importe from AbonosCreditos inner join Ventas on AbonosCreditos.idVenta = Ventas.idVenta  where Ventas.idCliente = " & txtIdCliente.Text & ""
        lector = command.ExecuteReader
        While lector.Read
            dgAgregar.Rows.Add(lector(0), lector(1), lector(2), lector(3))
        End While
        lector.Close()

        Dim id As Integer
        For i = 0 To dgAgregar.Rows.Count - 1
            id = dgAgregar.Item(1, i).Value
            If Not cbVentas.Items.Contains(id) Then
                cbVentas.Items.Add(id)
            End If
        Next
    End Sub

    Private Sub cbVentas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbVentas.SelectedIndexChanged
        dgAgregar.Rows.Clear()
        command.CommandText = "SELECT idAbonoC, fecha, importe from AbonosCreditos where idVenta = " & cbVentas.Text & ""
        lector = command.ExecuteReader
        While lector.Read
            dgAgregar.Rows.Add(lector(0), cbVentas.Text, lector(1), lector(2))
        End While
        lector.Close()
    End Sub
End Class