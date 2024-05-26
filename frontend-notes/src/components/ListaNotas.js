import React,{useState,useEffect, Fragment} from 'react'
import Table from 'react-bootstrap/Table';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import axios from 'axios';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import { Container } from 'react-bootstrap';
import NuevaNota from './NuevaNota';
import Filter from './Filter';

const ListadoNotas = () => {

    const [showDetails, setShowDetails] = useState(false);
    const [showEdit, setShowEdit] = useState(false);
    //para el item seleccionado
    const [selectedItem, setSelectedItem] = useState(null);

    const [id, setId] = useState('')
    const [title, setTitle] = useState('')
    const [description, setDescription] = useState('')
    const [idAccount, setIdAccount] = useState('')
    const [isArchive, setIsArchive] = useState(0)
    const [idTag, setIdTag] = useState('')

    const [editId, setEditId] = useState('')
    const [editTitle, setEditTitle] = useState('')
    const [editDescription, setEditDescription] = useState('')
    const [editIdAccount, setEditIdAccount] = useState('')
    const [editIsArchive, setEditIsArchive] = useState(0)
    const [editIdTag, setEditIdTag] = useState('')

    // Filter
    const [filteredData, setFilteredData] = useState([]);
    const handleFilterChange = (filteredData) => {
        setFilteredData(filteredData);
    };

    // Data
    const [data,setData] = useState([]);

    useEffect(()=>{
        getData();
    },[])
    const getData = async () =>{
        const respuesta = await axios.get('https://localhost:7259/Notes')
        .then((respuesta)=>{
            setData(respuesta.data);
        })
        .catch((error)=>{
            console.log("Error al obtener la información de las notas")
        })
    }
    // Limpiar inputs
    const clear = () => { 
        setTitle('');
        setDescription('');
        setIdAccount(1);
        setIsArchive(0)
        setIdTag(1);
        setId(0)
        setEditTitle('');
        setEditDescription('');
        setEditIdAccount(1);
        setEditIsArchive(0)
        setEditIdTag(1);
        setEditId(0)
    }
    // Handle Show
    const handleShowDetails =(item) => {
        setShowDetails(true);
        setSelectedItem(item);
    };
    const handleShowEdit = (item) => {
        setShowEdit(true);
        setSelectedItem(item);
    };
    // Handle Close
    const handleCloseDetails = () => {
        setShowDetails(false);
        setSelectedItem(null);
    };
    const handleCloseEdit = () => {
        setShowEdit(false);
        setSelectedItem(null);
    };
    // Handle Eliminar - Editar - Actualizar
    const handleDelete = (id, title) => {
        if (window.confirm("Are you sure to delete this note?") === true) {
            axios.delete(`https://localhost:7259/Notes/${id}`)
            .then((result)=>{
    
                if (result.status === 200) {
                    toast.success(`${title} has been deleted`);
                    getData();
                }
            })
            .catch((error) => {
                console.log(id)
                console.error("Error deleting note:", error);
                toast.error("Error deleting note. Please try again.");
            });
        }
        }

    const handleEdit = (id) => { //para editar notas
        handleShowEdit(true);
        axios.get(`https://localhost:7259/Notes/${id}`)
        .then((result) => {
            setEditTitle(result.data.title);
            setEditDescription(result.data.description);
            setEditIdAccount(result.data.idAccount);
            setEditIsArchive(result.data.isArchive);
            setEditIdTag(result.data.idTag);
            setEditId(id)
        })
        .catch((error)=>{
            toast.error(error);
        })
    }

    const handleUpdate = (title) => {
        const url = `https://localhost:7259/Notes/${editId}`
        const data = {
            "id":editId,
            "title": editTitle,
            "description": editDescription,
            "isArchive": editIsArchive,
            "idAccount": 1,
            "idTag": 1
        }
        axios.put(url,data)
        .then((result) => {
            handleCloseEdit();
            getData();
            clear();
            toast.success('Note has been updated')
        }).catch((error) => {
            toast.error(error);
        });
    }


    

    



    const handleEditArchiveChange = (e) => {
        if (e.target.checked){
            setEditIsArchive(1)
        }
        else{
            setEditIsArchive(0)
        }
    }



  return (
    <Fragment>
        
        <NuevaNota></NuevaNota>
        <br /><br />

        <Container>
            <Row>
                <Col sm = {2} md = {2} lg = {2} xl = {2} xxl = {2}>
                    <Filter onFilterChange={handleFilterChange} />
                </Col>

                <Col>
                    <Table striped bordered hover>
                        <thead>
                            <tr>
                                <th>#</th>
                                <th>Title</th>
                                <th>Tag</th>
                                <th>Acciones</th>
                            </tr>
                        </thead>
                        <tbody>
                        {
                            filteredData.length > 0 ? 
                            filteredData.map((item, index) => {
                                return (
                                        <tr key={index}>
                                            <td>{index+1}</td>
                                            <td>{item.title}</td>
                                            <td>{item.tag}</td>
                                            <td colSpan={2}>                                                    
                                                <button className='btn btn-primary' onClick={() => handleShowDetails(item)}>Details</button> &nbsp;
                                                <button className='btn btn-primary' onClick={() => handleEdit(item.id,item.title)}>Edit</button> &nbsp;
                                                <button className='btn btn-danger' onClick={() => handleDelete(item.id,item.title)}>Delete</button> &nbsp;
                                            </td>
                                        </tr>
                                    )
                                })
                                :
                                'Seleccione un filtro'
                            }
                        </tbody>
                    </Table>
                </Col>
            </Row>
        </Container>

        {/* // Modal Detalles */}
      <Modal show={showDetails} onHide={handleCloseDetails}>
        <Modal.Header closeButton>
          <Modal.Title>Detalle</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          {selectedItem && (
            <div>
              <h6>{selectedItem.description}</h6>
            </div>
          )}
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={handleCloseDetails}>
            Cerrar
          </Button>
        </Modal.Footer>
      </Modal>

        <Modal show={showEdit} onHide={handleCloseEdit}>
            <Modal.Header closeButton>
                <Modal.Title>Modify Note</Modal.Title>
            </Modal.Header>
            <Modal.Body>
            <Row>
                <Col md={4}>
                    <input type="text" className='form-control' placeholder='Enter Title'
                        value={editTitle} onChange={(e) => setEditTitle(e.target.value)}
                    />
                </Col>
                <Col md={4}>
                    <input type="text" className='form-control' placeholder='Enter Note'
                        value={editDescription} onChange={(e) => setEditDescription(e.target.value)}
                    />
                </Col>
                <Col md={4}>
                    <input type="checkbox" checked={editIsArchive === 1 ? true : false}
                        onChange={(e) => handleEditArchiveChange(e)} value={editIsArchive}
                    />
                    <label>isArchive</label>
                </Col>
            </Row>
            </Modal.Body>
            <Modal.Footer>
                <Button variant="secondary" onClick={handleCloseEdit}>
                    Close
                </Button>
                <Button variant="primary" onClick={handleUpdate}>
                    Save Changes
                </Button>
            </Modal.Footer>
        </Modal>
    </Fragment>
  )
}
export default ListadoNotas;