import { useContext, useState } from 'react'
import { Alert, Button, Empty, Spin, message } from 'antd'
import EmployeeCard from '../../components/EmployeeCard/EmployeeCard'
import DeleteConfirmModal from '../../components/DeleteConfirmModal/DeleteConfirmModal'
import EmployeeModalContext from '../../contexts/EmployeeModalContext'
import { useDeleteEmployee } from '../../hooks/useDeleteEmployee'
import { useEmployees } from '../../hooks/useEmployees'
import type { Employee } from '../../types/EmployeeTypes'
import './EmployeesList.scss'

const EmployeesList = () => {
  const [employeeToDelete, setEmployeeToDelete] = useState<Employee | null>(null)
  const [messageApi, contextHolder] = message.useMessage()
  const { openCreateEmployeeModal, openEditEmployeeModal } = useContext(EmployeeModalContext)
  const { data: employees = [], isLoading, isError } = useEmployees()
  const deleteEmployee = useDeleteEmployee()
  const employeesList = Array.isArray(employees) ? employees : []

  const handleConfirmDelete = () => {
    if (!employeeToDelete?.id) {
      return
    }

    deleteEmployee.mutate(employeeToDelete.id, {
      onSuccess: () => {
        messageApi.success('Employee deleted')
        setEmployeeToDelete(null)
      },
      onError: () => {
        messageApi.error('Delete failed')
      },
    })
  }

  return (
    <section className="page employees-list-page">
      {contextHolder}

      <div className="page__header">
        <div>
          <h1 className="page__title">Employees</h1>
          <p className="page__subtitle">Total: {employeesList.length}</p>
        </div>
        <Button type="primary" onClick={openCreateEmployeeModal}>
          Add Employee
        </Button>
      </div>

      {isLoading && <Spin />}
      {isError && <Alert type="error" message="Employees could not be loaded." />}

      {!isLoading && !isError && employeesList.length === 0 && (
        <Empty description="No employees" />
      )}

      <div className="employees-list-page__list">
        {employeesList.map((employee) => (
          <EmployeeCard
            key={employee.id}
            employee={employee}
            onEdit={openEditEmployeeModal}
            onDelete={setEmployeeToDelete}
          />
        ))}
      </div>

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

export default EmployeesList
