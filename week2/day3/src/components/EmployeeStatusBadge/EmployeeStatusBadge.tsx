import './EmployeeStatusBadge.scss'

type EmployeeStatusBadgeProps = {
  active?: boolean | null
}

const EmployeeStatusBadge = ({ active }: EmployeeStatusBadgeProps) => {
  const isActive = active === true

  return (
    <span
      className={
        isActive
          ? 'employee-status employee-status--active'
          : 'employee-status employee-status--inactive'
      }
    >
      {isActive ? 'active' : 'not active'}
    </span>
  )
}

export default EmployeeStatusBadge
