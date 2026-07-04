import { useContext } from 'react'
import { NavLink } from 'react-router-dom'
import { Button } from 'antd'
import EmployeeModalContext from '../../contexts/EmployeeModalContext'
import './Navbar.scss'

const Navbar = () => {
  const { openCreateEmployeeModal } = useContext(EmployeeModalContext)

  return (
    <header className="navbar">
      <NavLink to="/" className="navbar__brand">
        Employee App
      </NavLink>

      <nav className="navbar__links">
        <NavLink to="/" className="navbar__link">
          Employees
        </NavLink>
        <NavLink to="/about" className="navbar__link">
          About
        </NavLink>
      </nav>

      <Button type="primary" onClick={openCreateEmployeeModal}>
        Add Employee
      </Button>
    </header>
  )
}

export default Navbar
