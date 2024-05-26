import React,{useState,useEffect, Fragment} from 'react'
import axios from 'axios';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import Container from 'react-bootstrap/Container';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';

const NuevaNota = () => {

    const [id, setId] = useState(0)
    const [title, setTitle] = useState('')
    const [description, setDescription] = useState('')
    const [idAccount, setIdAccount] = useState(1)
    const [isArchive, setIsArchive] = useState(0)
    const [idTag, setIdTag] = useState(1)

    const [editId, setEditId] = useState('')
    const [editTitle, setEditTitle] = useState('')
    const [editDescription, setEditDescription] = useState('')
    const [editIdAccount, setEditIdAccount] = useState('')
    const [editIsArchive, setEditIsArchive] = useState(0)
    const [editIdTag, setEditIdTag] = useState('')

    const [data,setData] = useState([]);

    const getData = () =>{
        axios.get('https://localhost:7259/Notes')
        .then((result)=>{
            setData(result.data)
        })
        .catch((error)=>{
            console.log("Error al obtener la información de las notas")
        })
    }

    const clear = () => { //limpiar los input 
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

    const handleSave =() =>{ //para crear nuevas notas
        
        const url = 'https://localhost:7259/Notes'
        const data = {
            "id":0,
            "title": title,
            "description": description,
            "isArchive": isArchive,
            "idAccount": 1,
            "idTag": 1
        }
        axios.post(url,data)
        .then((result) => {
            clear();
            toast.success('Note has been added')
            
        })
        .catch((error) => {
            console.error("Error creating note:", error);
            toast.error("Error creating note. Please try again.");
        });
    }

    useEffect(()=>{
        getData();
    },[])

    //cambia el estado del checkbox de si está archivado o no
    const handleArchiveChange = (e) => {
        if (e.target.checked){
            setIsArchive(1)
        }
        else{
            setIsArchive(0)
        }
    }
    
    //

  return (
    <Fragment>
        <ToastContainer />

<Container>
    <Row>
        <Col md={3}>
            <input type="text" className='form-control' placeholder='Enter Title'
                value={title} onChange={(e) => setTitle(e.target.value)}
            />
        </Col>
        <Col md={3}>
            <input type="text" className='form-control' placeholder='Enter Note'
                value={description} onChange={(e) => setDescription(e.target.value)}
            />
        </Col>
        <Col md={3}>
            <input type="checkbox" checked={isArchive === 1 ? true : false}
                onChange={(e) => handleArchiveChange(e)} value={isArchive}
            />
            <label>isArchive</label>
        </Col>
        <Col md={3}>
            <button className='btn btn-primary' onClick={()=>handleSave()}>Submit</button>
        </Col>
    </Row>
</Container>
    </Fragment>
  )
}

export default NuevaNota