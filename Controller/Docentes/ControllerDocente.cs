using Refuerzo2024.Model.DAO;
using Refuerzo2024.View.Docentes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Refuerzo2024.Controller.Docentes
{
    internal class ControllerDocente
    {
        ViewDocentes objDocente;

        public ControllerDocente(ViewDocentes objDocente)
        {
            this.objDocente = objDocente;

            //Manejar eventos que surjan en la vista
            objDocente.Load += new EventHandler(CargaInicial);
            objDocente.btnAgregarDocente.Click += new EventHandler(RegistrarDocente);
            objDocente.dgvDocente.CellClick += new DataGridViewCellEventHandler(SeleccionarDato);
            objDocente.btnActualizar.Click += new EventHandler(ActualizarDocente);
            objDocente.btnEliminar.Click += new EventHandler(EliminarDocente);
            objDocente.btnBuscar.Click += new EventHandler(BuscarDocente);
        }

        public void CargaInicial(object sender, EventArgs e)
        {
          
            LlenarDataGridViewDocente();
        }
        public void SeleccionarDato(object sender, DataGridViewCellEventArgs e)
        {
            //Capturar la fila a la que se le dió click
            int pos = objDocente.dgvDocente.CurrentRow.Index;
            //Enviar los datos del DataGridView hacia los controles
            objDocente.txtIDDocente.Text = objDocente.dgvDocente[0, pos].Value.ToString();
            objDocente.txtNombresDocente.Text = objDocente.dgvDocente[1, pos].Value.ToString();
            objDocente.txtApellidosDocente.Text = objDocente.dgvDocente[2, pos].Value.ToString();;
            objDocente.txtDocumentoDocente.Text = objDocente.dgvDocente[6, pos].Value.ToString();
            
        }

        public void RegistrarDocente(object sender, EventArgs e)
        {
            DAODocente data = new DAODocente();
            //Guardar en los atributos del DTO todos los valores contenidos en los componentes del formulario
            data.NombreDocente = objDocente.txtNombresDocente.Text.Trim();
            data.ApellidoDocente = objDocente.txtApellidosDocente.Text.Trim();
            data.Dui = objDocente.txtDocumentoDocente.Text.Trim();
            //Se invoca al metodo RegistrarDocente y se verifica si su retorno es TRUE, de ser así significa que los datos pudieron ser registrados correctamente, de lo contrario, no se pudo registrar los valores.
            if (data.RegistrarDocente() == true)
            {
                MessageBox.Show("Datos registrados correctamente", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No se pudo guardar los datos", "Proceso incompleto", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LlenarDataGridViewDocente()
        {
            //Se crea objeto del DAOEstudiantes para accesar a todos los metodos contenidos en la clase.
            DAODocente obj = new DAODocente();
            //Se crea un DataSet que almacenará los valores que retorne el metodo.
            DataSet ds = obj.ObtenerDocente();
            //Llenamos el combobox
            objDocente.dgvDocente.DataSource = ds.Tables["Docentes"];
        }
        public void ActualizarDocente(object sender, EventArgs e)
        {
            DAODocente data = new DAODocente();
            //Guardar en los atributos del DTO todos los valores contenidos en los componentes del formulario
            data.IdDocente = int.Parse(objDocente.txtIDDocente.Text.Trim().ToString());
            data.NombreDocente = objDocente.txtNombresDocente.Text.Trim();
            data.ApellidoDocente = objDocente.txtApellidosDocente.Text.Trim();
            data.Dui = objDocente.txtDocumentoDocente.Text.Trim();
            if (data.ActualizarDocente() == true)
            {
                MessageBox.Show("Los datos fueron actualizados correctamente", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LlenarDataGridViewDocente();
            }
            else
            {
                MessageBox.Show("Los datos no pudieron ser actualizados.", "Proceso interrumpido", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void EliminarDocente(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(objDocente.txtIDDocente.Text.Trim()))
            {
                MessageBox.Show("Seleccione un registro", "Seleccione un valor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DAODocente data = new DAODocente();
                data.IdDocente = int.Parse(objDocente.txtIDDocente.Text);
                if (MessageBox.Show("¿Desea eliminar el registro seleccionado?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (data.EliminarDocente() == true)
                    {
                        MessageBox.Show("El dato fue eliminado correctamente", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LlenarDataGridViewDocente();
                    }
                    else
                    {
                        MessageBox.Show("El registro no pudo ser eliminado", "Proceso interrumpido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        public void BuscarDocente(object sender, EventArgs e)
        {
            DAODocente data = new DAODocente();
            DataSet ds = data.BuscarDocente(objDocente.txtBuscar.Text.Trim());
            objDocente.dgvDocente.DataSource = ds.Tables["Docentes"];
        }

    }
}
