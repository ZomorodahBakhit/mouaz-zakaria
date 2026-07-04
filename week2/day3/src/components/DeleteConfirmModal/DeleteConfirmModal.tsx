import { Modal } from 'antd'
import type { Employee } from '../../types/EmployeeTypes'
import './DeleteConfirmModal.scss'

type DeleteConfirmModalProps = {
  employee: Employee | null
  open: boolean
  confirmLoading?: boolean
  onCancel: () => void
  onConfirm: () => void
}

const DeleteConfirmModal = ({
  employee,
  open,
  confirmLoading = false,
  onCancel,
  onConfirm,
}: DeleteConfirmModalProps) => {
  return (
    <Modal
      open={open}
      title="Delete employee"
      okText="Delete"
      okButtonProps={{ danger: true }}
      confirmLoading={confirmLoading}
      onCancel={onCancel}
      onOk={onConfirm}
    >
      <p className="delete-confirm-modal__text">
        {employee ? `Delete ${employee.name}?` : 'Delete this employee?'}
      </p>
    </Modal>
  )
}

export default DeleteConfirmModal
