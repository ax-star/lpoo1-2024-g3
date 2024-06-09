﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ClasesBase;

namespace Vistas
{
    public partial class FrmUsuario : Form
    {
        public FrmUsuario()
        {
            InitializeComponent();
        }

        private void FrmUsuario_Load(object sender, EventArgs e)
        {
            Cargar_gridUsuarios();
            Cargar_combRol();
        }

        private void Cargar_gridUsuarios()
        {
            gridUsuarios.DataSource = TrabajarUsuario.ListarUsuarios();
        }

        private void Cargar_combRol()
        {
            combRol.DisplayMember = "Descripcion";
            combRol.ValueMember = "Codigo";
            combRol.DataSource = TrabajarUsuario.ListarRoles();
        }

        private void gridUsuarios_CurrentCellChanged(object sender, EventArgs e)
        {
            if (gridUsuarios.CurrentRow != null)
            {
                combRol.SelectedValue = gridUsuarios.CurrentRow.Cells["Codigo Rol"].Value;
                txtUsuario.Text = gridUsuarios.CurrentRow.Cells["Usuario"].Value.ToString();
                txtContrasenia.Text = gridUsuarios.CurrentRow.Cells["Contraseña"].Value.ToString();
                txtApellidoNombre.Text = gridUsuarios.CurrentRow.Cells["Apellido y Nombre"].Value.ToString();

                btnActualizar.Enabled = true;
                btnEliminar.Enabled = true;
                btnGuardar.Enabled = false;
            }
        }

        private Usuario ExtraerUsuario()
        {
            Usuario oUsu = new Usuario();
            oUsu.Usu_ID = (int) gridUsuarios.CurrentRow.Cells["ID"].Value;
            oUsu.Usu_NombreUsuario = txtUsuario.Text;
            oUsu.Usu_Contraseña = txtContrasenia.Text;
            oUsu.Usu_ApellidoNombre = txtApellidoNombre.Text;
            oUsu.Rol_Codigo = (int) combRol.SelectedValue;
            return oUsu;
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            TrabajarUsuario.ActualizarUsuario(ExtraerUsuario());
            Cargar_gridUsuarios();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("Desea eliminar el registro del Usuario: "+txtUsuario.Text, "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (resultado==DialogResult.Yes)
            {
                TrabajarUsuario.EliminarUsuario(ExtraerUsuario());
                Cargar_gridUsuarios();
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            TrabajarUsuario.InsertarUsuario(ExtraerUsuario());
            Cargar_gridUsuarios();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            combRol.SelectedValue = 1;
            txtUsuario.Text = null;
            txtContrasenia.Text = null;
            txtApellidoNombre.Text = null;

            btnActualizar.Enabled = false;
            btnEliminar.Enabled = false;
            btnGuardar.Enabled = true;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBuscador.Text))
            {
                Cargar_gridUsuarios();
            } else
            {
                gridUsuarios.DataSource = TrabajarUsuario.BuscarUsuarios(txtBuscador.Text);
            }
        }
    }
}
