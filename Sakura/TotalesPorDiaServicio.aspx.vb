﻿Public Class TotalesPorDiaServicio
    Inherits System.Web.UI.Page
    Public Sub MonstrarPopUp(Pagina As String, Titulo As String)
        Try
            Dim funcion As String = "PopupCenter('" & Pagina & "', " & "'" & Titulo & "',900,500);"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "PopupCenter", funcion, True)
        Catch ex As Exception
            MostrarMensaje(ex.Message, True)
        End Try
    End Sub
    Private Sub MostrarMensaje(ByVal texto As String, EsError As Boolean)
        Try
            texto = texto.Replace("'", "")
            Dim funcion As String = "Mensajes('" & texto & "" & "','" & IIf(EsError, "1", "0") & "');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Mensajes", funcion, True)
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Dim odatos As New CapaDatos.DataAcces
                Dim tabla As DataTable = odatos.RecuperarTiposdeServicios().Tables(0)
                txtTipoServicio.DataSource = tabla
                txtTipoServicio.DataTextField = "Descripcion"
                txtTipoServicio.DataValueField = "IdTipoServicio"
                txtTipoServicio.DataBind()
                odatos.Dispose()
            End If
        Catch ex As Exception
            MostrarMensaje(ex.Message, True)
        End Try
    End Sub

    Private Sub btnbuscar_Click(sender As Object, e As EventArgs) Handles btnbuscar.Click
        Try
            If DateDiff(DateInterval.Day, CDate(txtFechaServicioInicial.Text), CDate(txtFechaServicioFinal.Text)) > 365 Then
                MostrarMensaje("El intervalo de fechas no debe ser mayor a un año", True)
                Return
            End If

            Session("Finicial") = txtFechaServicioInicial.Text
            Session("Ffinal") = txtFechaServicioFinal.Text
            Session("TipoServicio") = txtTipoServicio.SelectedValue
            Session("Texto") = txtTipoServicio.SelectedItem.Text.ToString()
            If String.IsNullOrEmpty(txtformato.Text) Then
                MostrarMensaje("Debe escoger un formato para exportar la informacion", 1)
                Return
            End If
            Session("Formato") = txtformato.Text
            MonstrarPopUp("Reportes/TotalesPorServicioyDia.aspx", "Total asistencia dominical")
        Catch ex As Exception
            MostrarMensaje(ex.Message, True)
        End Try
    End Sub
End Class