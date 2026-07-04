import { useContext, useState } from 'react'
import { Link, useNavigate, useParams } from 'react-router-dom'
import { Alert, Button, Descriptions, Spin, message } from 'antd'
import DeleteConfirmModal from '../../components/DeleteConfirmModal/DeleteConfirmModal'
import EmployeeStatusBadge from '../../components/EmployeeStatusBadge/EmployeeStatusBadge'
import EmployeeModalContext from '../../contexts/EmployeeModalContext'
import { useDeleteEmployee } from '../../hooks/useDeleteEmployee'
import { useEmployee } from '../../hooks/useEmployee'
import type { Employee } from '../../types/EmployeeTypes'
import './EmployeeDetail.scss'

const EmployeeDetail = () => {
  const { id } = useParams()
  const navigate = useNavigate()
  const [messageApi, contextHolder] = message.useMessage()
  const [employeeToDelete, setEmployeeToDelete] = useState<Employee | null>(null)
  const { openEditEmployeeModal } = useContext(EmployeeModalContext)
  const { data: employee, isLoading, isError } = useEmployee(id)
  const deleteEmployee = useDeleteEmployee()

  const handleConfirmDelete = () => {
    if (!employeeToDelete?.id) {
      return
    }

    deleteEmployee.mutate(employeeToDelete.id, {
      onSuccess: () => {
        messageApi.success('Employee deleted')
        setEmployeeToDelete(null)
        navigate('/')
      },
      onError: () => {
        messageApi.error('Delete failed')
      },
    })
  }

  return (
    <section className="page employee-detail-page">
      {contextHolder}
      <Link to="/">Back to employees</Link>

      <div className="page__header">
        <div>
          <h1 className="page__title">Employee Details</h1>
          <p className="page__subtitle">Employee information</p>
        </div>

        {employee && (
          <div className="employee-detail-page__actions">
            <Button onClick={() => openEditEmployeeModal(employee)}>Edit</Button>
            <Button danger onClick={() => setEmployeeToDelete(employee)}>
              Delete
            </Button>
          </div>
        )}
      </div>

      {isLoading && <Spin />}
      {isError && <Alert type="error" message="Employee could not be loaded." />}

      {employee && (
        <Descriptions bordered column={1}>
          <Descriptions.Item label="Name">{employee.name}</Descriptions.Item>
          <Descriptions.Item label="Email">{employee.email}</Descriptions.Item>
          <Descriptions.Item label="Department">{employee.department}</Descriptions.Item>
          <Descriptions.Item label="Salary">${employee.salary}</Descriptions.Item>
          <Descriptions.Item label="Active">
            <EmployeeStatusBadge active={employee.active} />
          </Descriptions.Item>
        </Descriptions>
      )}

      <DeleteConfirmModal
        open={Boolean(employeeToDelete)}
        employee={employeeToDelete}
        confirmLoading={deleteEmployee.isPending}
        onCancel={() => setEmployeeToDelete(null)}
        onConfirm={handleConfirmDelete}
      />
    </section>
  )
}

export default EmployeeDetail
