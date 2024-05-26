import React, { useState, useEffect } from 'react';
import axios from 'axios';

const Filter = ({ onFilterChange }) => {
  const [archiveSelect, setArchiveSelect] = useState({
    1: false,
    0: false,
  });

  const [selectedValue, setSelectedValue] = useState(null);
  const [dataFiltered, setDataFiltered] = useState([]);
  const [data, setData] = useState([]);

  useEffect(() => {
    getData();
  }, []);

  const getData = () => {
    axios.get('https://localhost:7259/Notes')
      .then((result) => {
        setData(result.data);
      })
      .catch((error) => {
        console.log("Error al obtener la información de las notas");
      });
  };

  const handleOnRadioChange = (e) => {
    const value = e.target.value;
    setSelectedValue(value);
    setArchiveSelect({
      1: value === "1",
      0: value === "0",
    });
    setFilteredData(value);
  };

  const setFilteredData = (value) => {
    let resultArchive = [];

    if (value === null) {
      resultArchive = [...data];
    } else {
      resultArchive = data.filter(
        (item) => item.isArchive.toString() === value
      );
    }

    setDataFiltered(resultArchive);
    onFilterChange(resultArchive);
    console.log(resultArchive)
  };

  return (
    <div>
      <div className="radio-container">
        <h2>Filtros</h2>
        <div className="input-radio form-check">
          <input
            onChange={handleOnRadioChange}
            type="radio"
            name="isArchive"
            value="1"
            id="1"
            className="form-check-input"
            checked={selectedValue === "1"}
          />
          <label className="form-check-label" htmlFor="1">
            Archive
          </label>
        </div>

        <div className="input-radio form-check">
          <input
            onChange={handleOnRadioChange}
            type="radio"
            name="isArchive"
            value="0"
            id="0"
            className="form-check-input"
            checked={selectedValue === "0"}
          />
          <label className="form-check-label" htmlFor="0">
            Unarchive
          </label>
        </div>
        <div>
          {/* RADIO BUTTON PARA LOS TAGS */}
        </div>
      </div>
    </div>
  );
};

export default Filter;