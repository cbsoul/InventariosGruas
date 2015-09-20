<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class NuevoVehiculo
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtMarca = New System.Windows.Forms.TextBox()
        Me.txtModelo = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.marcaCB = New System.Windows.Forms.ComboBox()
        Me.conocidaCHB = New System.Windows.Forms.CheckBox()
        Me.nuevaCHB = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 33)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(51, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Marca:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 126)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(40, 17)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Tipo:"
        '
        'txtMarca
        '
        Me.txtMarca.Location = New System.Drawing.Point(72, 45)
        Me.txtMarca.Name = "txtMarca"
        Me.txtMarca.Size = New System.Drawing.Size(148, 22)
        Me.txtMarca.TabIndex = 2
        '
        'txtModelo
        '
        Me.txtModelo.Location = New System.Drawing.Point(72, 123)
        Me.txtModelo.Name = "txtModelo"
        Me.txtModelo.Size = New System.Drawing.Size(148, 22)
        Me.txtModelo.TabIndex = 3
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(321, 93)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(85, 50)
        Me.Button1.TabIndex = 4
        Me.Button1.Text = "Nuevo Vehículo"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'marcaCB
        '
        Me.marcaCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.marcaCB.FormattingEnabled = True
        Me.marcaCB.Location = New System.Drawing.Point(72, 15)
        Me.marcaCB.Name = "marcaCB"
        Me.marcaCB.Size = New System.Drawing.Size(148, 24)
        Me.marcaCB.TabIndex = 5
        '
        'conocidaCHB
        '
        Me.conocidaCHB.AutoSize = True
        Me.conocidaCHB.Location = New System.Drawing.Point(226, 17)
        Me.conocidaCHB.Name = "conocidaCHB"
        Me.conocidaCHB.Size = New System.Drawing.Size(89, 21)
        Me.conocidaCHB.TabIndex = 6
        Me.conocidaCHB.Text = "Conocida"
        Me.conocidaCHB.UseVisualStyleBackColor = True
        '
        'nuevaCHB
        '
        Me.nuevaCHB.AutoSize = True
        Me.nuevaCHB.Location = New System.Drawing.Point(226, 44)
        Me.nuevaCHB.Name = "nuevaCHB"
        Me.nuevaCHB.Size = New System.Drawing.Size(71, 21)
        Me.nuevaCHB.TabIndex = 7
        Me.nuevaCHB.Text = "Nueva"
        Me.nuevaCHB.UseVisualStyleBackColor = True
        '
        'NuevoVehiculo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(414, 158)
        Me.Controls.Add(Me.nuevaCHB)
        Me.Controls.Add(Me.conocidaCHB)
        Me.Controls.Add(Me.marcaCB)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.txtModelo)
        Me.Controls.Add(Me.txtMarca)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "NuevoVehiculo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Nuevo Vehículo"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtMarca As System.Windows.Forms.TextBox
    Friend WithEvents txtModelo As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents marcaCB As System.Windows.Forms.ComboBox
    Friend WithEvents conocidaCHB As System.Windows.Forms.CheckBox
    Friend WithEvents nuevaCHB As System.Windows.Forms.CheckBox
End Class
