import { Button } from 'antd'
import { Link } from 'react-router-dom'
import EmployeeStatusBadge from '../EmployeeStatusBadge/EmployeeStatusBadge'
import type { Employee } from '../../types/EmployeeTypes'
import './EmployeeCard.scss'

type EmployeeCardProps = {
  employee: Employee
  onEdit: (employee: Employee) => void
  onDelete: (employee: Employee) => void
}

const EmployeeCard = ({ employee, onEdit, onDelete }: EmployeeCardProps) => {
  return (
    <div className="employee-card">
      <div className="employee-card__info">
        <div className="employee-card__header">
          <h2>{employee.name}</h2>
          <EmployeeStatusBadge active={employee.active} />
        </div>
        <p>{employee.department}</p>
        <p>{employee.email}</p>
        <p>${employee.salary}</p>
      </div>

      <div className="employee-card__actions">
        <Link to={`/employee/${employee.id}`}>View</Link>
        <Button onClick={() => onEdit(employee)}>Edit</Button>
        <Button danger onClick={() => onDelete(employee)}>
          Delete
        </Button>
      </div>
    </div>
  )
}

export default EmployeeCard
